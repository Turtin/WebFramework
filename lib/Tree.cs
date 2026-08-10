namespace Web_Framework.lib;

public class Tree<TS>
{
    public class Node<T>(Node<T> parent, string name)
    {
        public Dictionary<string, Node<T>> Children { get; } = new();
        public List<T> Data { get; } = new();
        public Node<T> Parent { get; } = parent;
        public string Name { get; } = name;
        
        /// <summary>
        /// This overloaded constructor allows for the creation of parentless nodes.
        /// Should only be used for creating nodes in the the tree head
        /// </summary>
        /// <param name="name">Name of the node to add</param>
        public Node(string name) : this(null, name)
        {
        }

        /// <summary>
        /// Gets a node from the tree, specifically one of the children of the current node based on the name of the child
        /// </summary>
        /// <param name="key">Name of the child node in question</param>
        /// <returns>The child node</returns>
        public Node<T> GetNode(string key)
        {
            return Children[key];
        }

        public void AddNode(string key, T value)
        {
            Children.Add(key, new Node<T>(this, key));
        }

        /// <summary>
        /// Adds data to the impediment head of the tree
        /// </summary>
        /// <param name="value">The data to be stored in the tree</param>
        public void AddData(T value)
        {
            Data.Add(value);
        }
    }
    
    public Dictionary<string, Node<TS>> Nodes { get; } = new();
    public List<TS> Data { get; } = new();
    
    /// <summary>
    /// Same as the nodes but stored in a slightly differently way to begin the node tree.
    /// Should use the overloaded contractor to create nodes for this
    /// </summary>
    /// <param name="key">The name of the given node</param>
    /// <param name="node">The node made using the overloaded contractor to add to the tree</param>
    public void AddNode(string key, Node<TS> node)
    {
        Nodes.Add(key, node);
    }
    
    /// <summary>
    /// Adds data to the impediment head of the tree
    /// </summary>
    /// <param name="value">The data to be stored in the tree</param>
    public void AddData(TS value)
    {
        Data.Add(value);
    }
    
    /// <summary>
    /// Gets a node from the tree head,
    /// Note that this specific node with not have a paranet as it belongs to the trees head
    /// </summary>
    /// <param name="key">Name of the node in question</param>
    /// <returns>The node with the given name</returns>
    public Node<TS> GetNode(string key)
    {
        return Nodes[key];
    }
}