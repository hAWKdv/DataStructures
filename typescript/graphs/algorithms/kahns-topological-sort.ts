import { Graph } from '../graph';

// Kahn's Algorithm
// This doesn't check if the graph is acyclic!
// G is presumably a DAG
function topologicalSort<T>(g: Graph<T>): T[] {
  g = g.copy();
  const sorted: T[] = [];
  const nonIncoming = g.vertices.filter((v: T) => !g.edges.find(([_, dest]) => dest === v));

  while (nonIncoming.length) {
    const v = nonIncoming.pop();
    sorted.push(v);

    for (let i = 0; i < g.edges.length; i += 1) {
      if (!g.edges[i]) {
        continue;
      }
      const [src, dest] = g.edges[i];
      if (src === v) {
        delete g.edges[i];
        if (!g.edges.filter(e => !!e).find(([_, d]) => d === dest)) {
          nonIncoming.push(dest);
        }
      }
    }
  }

  return sorted;
}

// Demo:

const graph = new Graph<string>();

graph.createVertex('A');
graph.createVertex('B');
graph.createVertex('C');
graph.createVertex('D');
graph.createVertex('E');
graph.createVertex('F');
graph.createVertex('G');
graph.createVertex('H');

graph.connectVertices('A', 'D', true);

graph.connectVertices('D', 'F', true);
graph.connectVertices('D', 'G', true);
graph.connectVertices('D', 'H', true);

graph.connectVertices('B', 'D', true);
graph.connectVertices('B', 'E', true);

graph.connectVertices('E', 'G', true);

graph.connectVertices('C', 'E', true);
graph.connectVertices('C', 'H', true);

const sorted = topologicalSort<string>(graph);
console.log(sorted);
