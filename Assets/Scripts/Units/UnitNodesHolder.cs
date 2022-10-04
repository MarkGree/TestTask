using System.Collections.Generic;

public class UnitNodesHolder
{
    private Dictionary<int, LinkedListNode<Unit>> nodes = new Dictionary<int, LinkedListNode<Unit>>();

    public void Add(int id, LinkedListNode<Unit> node)
    {
        nodes.Add(id, node);
    }

    public LinkedListNode<Unit> Remove(int id)
    {
        LinkedListNode<Unit> node;
       
        nodes.Remove(id, out node);
        
        return node;
    }
}
