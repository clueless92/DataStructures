from src.graph_playground.simple_graph import SimpleGraph

# TestGraph
# Node > 0   1   2   3   4   5   6   7   8   9  10  11
#   v
#   0    0,  0,  0,  0,  0,  0, 10,  0, 12,  0,  0,  0
#   1    0,  0,  0,  0, 20,  0,  0, 26,  0,  5,  0,  6
#   2    0,  0,  0,  0,  0,  0,  0, 15, 14,  0,  0,  9
#   3    0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  7,  0
#   4    0, 20,  0,  0,  0,  5, 17,  0,  0,  0,  0, 11
#   5    0,  0,  0,  0,  5,  0,  6,  0,  3,  0,  0, 33
#   6    0,  0,  0,  0, 17,  6,  0,  0,  0,  0,  0,  0
#   7    0, 26, 15,  0,  0,  0,  0,  0,  0,  3,  0, 20
#   8    2,  0, 14,  0,  0,  3,  0,  0,  0,  0,  0,  0
#   9    0,  5,  0,  0,  0,  0,  0,  3,  0,  0,  0,  0
#   10   0,  0,  0,  7,  0,  0,  0,  0,  0,  0,  0,  0
#   11   0,  6,  9,  0, 11, 33,  0, 20,  0,  0,  0,  0


def fill_graph():
    for i in range(0, graph.size()):
        graph.add_node(i)
    graph.add_edge(0, 6, 10)
    graph.add_edge(0, 8, 12)
    graph.add_edge(1, 4, 20)
    graph.add_edge(1, 7, 26)
    graph.add_edge(1, 9, 5)
    graph.add_edge(1, 11, 6)
    graph.add_edge(2, 7, 15)
    graph.add_edge(2, 8, 14)
    graph.add_edge(2, 11, 9)
    graph.add_edge(3, 10, 7)
    graph.add_edge(4, 5, 5)
    graph.add_edge(4, 6, 17)
    graph.add_edge(4, 11, 11)
    graph.add_edge(5, 6, 6)
    graph.add_edge(5, 8, 3)
    graph.add_edge(5, 11, 33)
    graph.add_edge(7, 9, 3)
    graph.add_edge(7, 11, 20)


def print_path(graph, source_node, destination_node):
    output = "Shortest path [{:d} -> {:d}]: ".format(source_node, destination_node)
    path = graph.dijkstra(source_node, destination_node)
    if path is None:
        output += "no path"
    else:
        path_length = 0
        for i in range(0, len(path) - 1, 1):
            path_length += graph.get_weight(path[i], path[i + 1])
        for p in range(0, len(path), 1):
            output += "{:d}".format(path[p])
            if p != len(path) - 1:
                output += "->"
        output += " (length = {:d})".format(path_length)
    print(output)


graph = SimpleGraph(12)
fill_graph()
print("BFS:")
for node in graph.bfs_traversal(0):
    print(node)
print("DFS:")
for node in graph.dfs_traversal(0):
    print(node)
print("Dijkstra:")
print_path(graph, 0, 9)
print_path(graph, 0, 2)
print_path(graph, 0, 10)
print_path(graph, 0, 11)
print_path(graph, 0, 1)
