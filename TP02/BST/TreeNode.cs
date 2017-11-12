using System;
using System.Collections.Generic;
using System.Text;

namespace TP02
{
    public class TreeNode
    {
        public double key;
        public double value;
        public TreeNode left, right;

        public TreeNode(double key, double d)
        {
            this.key = key;
            value = d;
            left = null;
            right = null;
        }
    }
}
