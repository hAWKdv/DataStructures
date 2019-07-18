import { Vertex, Edge } from '../graph';
import { Tree, Node } from '../../trees/tree';

type MstEdge<T> = [Vertex<T, number>, number, Vertex<T, number>];

// Prim's Algorithm
function minimalSpanningTree<T>(v: Vertex<T, number>, totalVerticesNum: number): Tree<T> {
  const visited: Set<Vertex<T,number>> = new Set([v]);
  const nodes = [new Node<T>(v.value)];

  while (visited.size < totalVerticesNum) {
    const closestVertices: MstEdge<T>[] = [];

    visited.forEach((vt: Vertex<T, number>) => {
      const closest = vt.edges
        .filter(e => !visited.has(e.vertex))
        .sort((a, b) => b.weight - a.weight)
        .pop();

        if (closest) {
          closestVertices.push([vt, closest.weight, closest.vertex]);
        }
    });

    const grouped = closestVertices.reduce((obj, e: MstEdge<T>) => {
      const dest = e[2] as any as string;
      if (obj[dest]) {
        obj[dest].push(e);
      } else {
        obj[dest] = [e];
      }
      return obj;
    }, {});


    Object.keys(grouped).forEach(key => {
      const [ src, _, dest ]: MstEdge<T> = grouped[key]
        .sort((a: MstEdge<T>, b: MstEdge<T>) => b[1] - a[1])
        .pop();

      const node = nodes.find(n => n.value === src.value);
      const newNode = new Node(dest.value);

      node.children.push(newNode);
      nodes.push(newNode);
      visited.add(dest);
    });
  }

  return new Tree(nodes[0]);
}

// Demo:

const vertexA = new Vertex<string, number>('A');
const vertexB = new Vertex<string, number>('B');
const vertexC = new Vertex<string, number>('C');
const vertexD = new Vertex<string, number>('D');
const vertexE = new Vertex<string, number>('E');
const vertexF = new Vertex<string, number>('F');

vertexA
  .addAdjacentVertex(vertexB, false, 2)
  .addAdjacentVertex(vertexD, false, 5)
  .addAdjacentVertex(vertexE, false, 1);

vertexB
  .addAdjacentVertex(vertexD, false, 14)
  .addAdjacentVertex(vertexC, false, 8);

vertexD
  .addAdjacentVertex(vertexF, false, 1)
  .addAdjacentVertex(vertexE, false, 2)
  .addAdjacentVertex(vertexC, false, 10);

vertexE.addAdjacentVertex(vertexF, false, 7);

vertexC.addAdjacentVertex(vertexF, false, 3);

const tree = minimalSpanningTree<string>(vertexA, 6);
console.log(tree.toString());
