using System.Collections.Generic;

namespace Status
{
    public class StatusTreeParams
    {
        //public StatusTreeParams(BasePublicStatusNodes basePublicStatusNodes)
        //{
        //    PublicOutgameIntegerNode = basePublicStatusNodes.PublicOutgameIntegerNode;
        //    PublicOutgameRateNode = basePublicStatusNodes.PublicOutgameRateNode;
        //    PublicIngameIntegerNode = basePublicStatusNodes.PublicIngameIntegerNode;
        //    PublicIngameRateNode = basePublicStatusNodes.PublicIngameRateNode;
        //}

        public StatusNode PublicOutgameIntegerNode { get; }
        public StatusNode PublicOutgameRateNode { get; }
        public StatusNode PublicIngameIntegerNode { get; }
        public StatusNode PublicIngameRateNode { get; }
    }

    public class StatusTree
    {
        private StatusNode _baseNode;
        public StatusNode FinalRateNode { get; private set; }
        public StatusNode RootNode { get; private set; }
        public StatusNode PrivateOutgameIntegerNode { get; private set; }
        public StatusNode PrivateOutgameRateNode { get; private set; }
        public StatusNode PrivateIngameIntegerNode { get; private set; }
        public StatusNode PrivateIngameRateNode { get; private set; }

        public void Initialize(string rootNodeName, StatusTreeParams treeParams)
        {
            _baseNode = StatusNode.NewValueTypeNode(nameof(_baseNode), 0);
            FinalRateNode = StatusNode.NewValueTypeNode(nameof(FinalRateNode), 1);
            RootNode = StatusNode.NewMultTypeNode($"{nameof(RootNode)} : {rootNodeName}", FinalRateNode);
            PrivateOutgameIntegerNode = StatusNode.NewSumTypeNode(nameof(PrivateOutgameIntegerNode));
            PrivateOutgameRateNode = StatusNode.NewSumTypeNode(nameof(PrivateOutgameRateNode));
            PrivateIngameIntegerNode = StatusNode.NewSumTypeNode(nameof(PrivateIngameIntegerNode));
            PrivateIngameRateNode = StatusNode.NewSumTypeNode(nameof(PrivateIngameRateNode));

            var totalOutgameIntegerNode = StatusNode.NewSumTypeNode(
                "totalOutgameIntegerNode",
                new List<StatusNode>
                {
                    treeParams.PublicOutgameIntegerNode,
                    PrivateOutgameIntegerNode
                });
            var subresultOutgameIntegerNode = StatusNode.NewSumTypeNode(
                "subresultOutgameIntegerNode",
                new List<StatusNode>
                {
                    totalOutgameIntegerNode,
                    _baseNode
                });
            var totalOutgameRateNode = StatusNode.NewSumTypeNode(
                "totalOutgameRateNode",
                new List<StatusNode>
                {
                    StatusNode.NewValueTypeNode("default", 1),
                    treeParams.PublicOutgameRateNode,
                    PrivateOutgameRateNode
                });
            var subresultOutgameRateNode = StatusNode.NewMultTypeNode(
                "subresultOutgameRateNode",
                new List<StatusNode>
                {
                    totalOutgameRateNode,
                    subresultOutgameIntegerNode
                });

            var totalIngameIntegerNode = StatusNode.NewSumTypeNode(
                "totalIngameIntegerNode",
                new List<StatusNode>
                {
                    treeParams.PublicIngameIntegerNode,
                    PrivateIngameIntegerNode
                });
            var subresultIngameIntegerNode = StatusNode.NewSumTypeNode(
                "subresultIngameIntegerNode",
                new List<StatusNode>
                {
                    totalIngameIntegerNode,
                    subresultOutgameRateNode
                });
            var totalIngameRateNode = StatusNode.NewSumTypeNode(
                "totalIngameRateNode",
                new List<StatusNode>
                {
                    StatusNode.NewValueTypeNode("default", 1),
                    treeParams.PublicIngameRateNode,
                    PrivateIngameRateNode
                });
            var subresultIngameRateNode = StatusNode.NewMultTypeNode(
                "subresultIngameRateNode",
                new List<StatusNode>
                {
                    totalIngameRateNode,
                    subresultIngameIntegerNode
                });
            RootNode.AddChild(subresultIngameRateNode);
        }

        public void UpdateBaseValue(float value)
        {
            _baseNode.Value = value;
        }
    }
}