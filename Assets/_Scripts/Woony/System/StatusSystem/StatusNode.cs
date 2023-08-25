using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Status
{
    public enum StatusNodeType
    {
        Value,
        Sum,
        Multiply,
    }

    public class StatusNode
    {
        public string Name { get; private set; }
        public StatusNodeType StatusNodeType { get; private set; }
        public float _value;
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateValue();
            }
        }

        private Action _onModified;

        private List<StatusNode> _heads = new List<StatusNode>();
        private List<StatusNode> _beRemoveHeadsContainer = new List<StatusNode>();

        // only Sum or Multiply type variable
        [NonSerialized] private List<StatusNode> _children;
        //-----

        public static StatusNode NewValueTypeNode(string name, float value)
        {
            return new StatusNode(name, StatusNodeType.Value, value);
        }

        public static StatusNode NewSumTypeNode(string name)
        {
            return NewSumTypeNode(name, NewValueTypeNode("default", 0));
        }

        public static StatusNode NewSumTypeNode(string name, StatusNode child)
        {
            return new StatusNode(name, StatusNodeType.Sum, new List<StatusNode>() { child });
        }

        public static StatusNode NewSumTypeNode(string name, List<StatusNode> children)
        {
            return new StatusNode(name, StatusNodeType.Sum, children);
        }

        public static StatusNode NewMultTypeNode(string name)
        {
            return NewMultTypeNode(name, NewValueTypeNode("default", 1));
        }

        public static StatusNode NewMultTypeNode(string name, StatusNode child)
        {
            return new StatusNode(name, StatusNodeType.Multiply, new List<StatusNode>() { child });
        }

        public static StatusNode NewMultTypeNode(string name, List<StatusNode> children)
        {
            return new StatusNode(name, StatusNodeType.Multiply, children);
        }

        public StatusNode(string name, StatusNodeType statusNodeType, float value)
        {
            Name = name;
            StatusNodeType = statusNodeType;
            Value = value;
        }

        public StatusNode(string name, StatusNodeType statusNodeType, List<StatusNode> children)
        {
            Name = name;
            StatusNodeType = statusNodeType;
            SetChildren(children);
            UpdateValue();

            void SetChildren(List<StatusNode> children)
            {
                if (children == null || children.Count == 0) return;

                children.ForEach(x => x.UpdateHead(this));
                // log 호출 시 편한하게 보기 위한 type 정렬
                _children = children.OrderBy(x => x.StatusNodeType).ToList();
            }
        }

        public void ClearModifiedEvent()
        {
            _onModified = null;
        }

        /// <summary>
        /// 이벤트 변수 초기화해야 함에 주의하세요.
        /// </summary>
        /// <param name="onModified"></param>
        public void AddModifiedEvent(Action onModified)
        {
            _onModified -= onModified;
            _onModified += onModified;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateHead(StatusNode headNode)
        {
            if (this == headNode)
            {
                Debug.LogError("head는 자기 자신일 수 없다.");
                return;
            }
            if (_heads.Contains(headNode)) return;
            _heads.Add(headNode);
        }

        public void AddChild(StatusNode statusNode)
        {
            if (_children == null)
            {
                _children = new List<StatusNode>();
            }
            else if (!IsValidNode(statusNode)) return;

            statusNode.UpdateHead(this);
            _children.Add(statusNode);
            UpdateValue();

            bool IsValidNode(StatusNode statusNode)
                => !_children.Contains(statusNode)
                && this != statusNode
                && !_heads.Contains(statusNode);
        }

        private void UpdateValue()
        {
            switch (StatusNodeType)
            {
                case StatusNodeType.Value:
                    break;
                case StatusNodeType.Sum:
                    _value = 0;
                    _children.ForEach(x => _value += x.Value);
                    break;
                case StatusNodeType.Multiply:
                    _value = 1;
                    _children.ForEach(x => _value *= x.Value);
                    break;
                default:
                    break;
            }
            _heads.ForEach(x =>
            {
                if (x == null)
                {
                    _beRemoveHeadsContainer.Add(x);
                    return;
                }
                x.UpdateValue();
            });
            _beRemoveHeadsContainer.ForEach(x => _heads.Remove(x));
            _onModified?.Invoke();
        }

        public void RemoveChild(StatusNode removeTarget)
        {
            _children.Remove(removeTarget);
        }

        private int _tempIndex;
        private int _tempCount;
        public bool TryRemove(StatusNode removeTarget)
        {
            if (this == removeTarget)
            {
                Debug.LogError("자기 자신 삭제 불가");
                return false;
            }

            switch (StatusNodeType)
            {
                case StatusNodeType.Value:
                    break;
                case StatusNodeType.Sum:
                case StatusNodeType.Multiply:
                    _tempCount = _children.Count;
                    for (_tempIndex = 0; _tempIndex < _tempCount; _tempIndex++)
                    {
                        if (_children[_tempCount] == removeTarget)
                        {
                            RemoveChild(removeTarget);
                            return true;
                        }
                    }
                    break;
                default:
                    break;
            }

            return false;
        }

#if UNITY_EDITOR
        public void ForceUpdate()
        {
            UpdateValue();
        }

        public void LogTree()
        {
            Debug.Log(GetLog(0));
        }

        private string GetLog(int indent)
        {
            var result = "";
            for (int i = 0; i < indent; i++) result += "----";

            result += GetNodeState();

            if (StatusNodeType == StatusNodeType.Sum
                || StatusNodeType == StatusNodeType.Multiply)
            {
                _children.ForEach(x => result += x.GetLog(indent + 1));
            }

            return result;

            string GetNodeState()
            {
                return $"[{StatusNodeType.ToString().Substring(0, 3)}Type] : V = {Value} - {Name}\n";
            }
        }
#endif
    }
}