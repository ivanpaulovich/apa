using System;

namespace TP02.BST
{
    public class BinarySTree
    {
        private TreeNode root;
        private int _count = 0;

        public BinarySTree()
        {
            root = null;
            _count = 0;
        }
        
        public TreeNode Pesquisar(double key, out int comparacoes)
        {
            TreeNode np = root;
            double cmp;
            comparacoes = 0;

            while (np != null)
            {
                cmp = key - np.key;

                comparacoes++;

                if (cmp == 0)   
                    return np;

                comparacoes++;

                if (cmp < 0)
                    np = np.left;
                else
                    np = np.right;
            }

            return null;  // Return null to indicate failure to find name
        }

        private void Adicionar(TreeNode node, ref TreeNode tree)
        {
            if (tree == null)
                tree = node;
            else
            {
                double comparison = node.key - tree.key;
                if (comparison == 0)
                    throw new Exception();

                if (comparison < 0)
                {
                    Adicionar(node, ref tree.left);
                }
                else
                {
                    Adicionar(node, ref tree.right);
                }
            }
        }

        public TreeNode Inserir(double key, double d)
        {
            TreeNode node = new TreeNode(key, d);
            try
            {
                if (root == null)
                    root = node;
                else
                    Adicionar(node, ref root);
                _count++;
                return node;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private TreeNode ProcurarPai(double key, ref TreeNode parent)
        {
            TreeNode np = root;
            parent = null;
            double cmp;
            while (np != null)
            {
                cmp = key - np.key;
                if (cmp == 0)   // found !
                    return np;

                if (cmp < 0)
                {
                    parent = np;
                    np = np.left;
                }
                else
                {
                    parent = np;
                    np = np.right;
                }
            }
            return null;  // Return null to indicate failure to find name
        }

        public TreeNode ProcurarSucessor(TreeNode startNode, ref TreeNode parent)
        {
            parent = startNode;
            // Look for the left-most node on the right side
            startNode = startNode.right;
            while (startNode.left != null)
            {
                parent = startNode;
                startNode = startNode.left;
            }
            return startNode;
        }

        public void Retirar(double key)
        {
            TreeNode parent = null;
            // First find the node to delete and its parent
            TreeNode nodeToDelete = ProcurarPai(key, ref parent);
            if (nodeToDelete == null)
                throw new Exception("Unable to delete node: " + key.ToString());  // can't find node, then say so 

            // Three cases to consider, leaf, one child, two children

            // If it is a simple leaf then just null what the parent is pointing to
            if ((nodeToDelete.left == null) && (nodeToDelete.right == null))
            {
                if (parent == null)
                {
                    root = null;
                    return;
                }

                // find out whether left or right is associated 
                // with the parent and null as appropriate
                if (parent.left == nodeToDelete)
                    parent.left = null;
                else
                    parent.right = null;
                _count--;
                return;
            }

            // One of the children is null, in this case
            // delete the node and move child up
            if (nodeToDelete.left == null)
            {
                // Special case if we're at the root
                if (parent == null)
                {
                    root = nodeToDelete.right;
                    return;
                }

                // Identify the child and point the parent at the child
                if (parent.left == nodeToDelete)
                    parent.right = nodeToDelete.right;
                else
                    parent.left = nodeToDelete.right;
                nodeToDelete = null; // Clean up the deleted node
                _count--;
                return;
            }

            // One of the children is null, in this case
            // delete the node and move child up
            if (nodeToDelete.right == null)
            {
                // Special case if we're at the root			
                if (parent == null)
                {
                    root = nodeToDelete.left;
                    return;
                }

                // Identify the child and point the parent at the child
                if (parent.left == nodeToDelete)
                    parent.left = nodeToDelete.left;
                else
                    parent.right = nodeToDelete.left;
                nodeToDelete = null; // Clean up the deleted node
                _count--;
                return;
            }

            // Both children have nodes, therefore find the successor, 
            // replace deleted node with successor and remove successor
            // The parent argument becomes the parent of the successor
            TreeNode successor = ProcurarSucessor(nodeToDelete, ref parent);
            // Make a copy of the successor node
            TreeNode tmp = new TreeNode(successor.key, successor.value);
            // Find out which side the successor parent is pointing to the
            // successor and remove the successor
            if (parent.left == successor)
                parent.left = null;
            else
                parent.right = null;

            // Copy over the successor values to the deleted node position
            nodeToDelete.key = tmp.key;
            nodeToDelete.value = tmp.value;
            _count--;
        }
    }
}
