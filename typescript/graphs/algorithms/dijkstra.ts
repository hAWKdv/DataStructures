import { Vertex as GenericVertex } from '../graph';
import { Heap } from '../../trees/heap';
import { CompareSym } from '../../common/interfaces/Comparer';

type Vertex<T> = GenericVertex<T, number>;

function dijkstra<T>(from: Vertex<T>, to: Vertex<T>): number {
  const visited: Set<Vertex<T>> = new Set;
  const distances: Map<T, number> = new Map([[ from.value, 0 ]]);
  const compare = (a: Vertex<T>, sym: CompareSym, b: Vertex<T>) => {
    if (!a || !b) {
      return false;
    }
    if (sym === CompareSym.GT) {
      return distances.get(a.value) > distances.get(b.value);
    } else {
      throw new Error('Comparison not implemented');
    }
  };
  const heap = new Heap(from, compare);

  while (!heap.empty) {
    const v = heap.extractMin();
    visited.add(v);
    const distance = distances.get(v.value);
    const nonVisited = v.edges.filter(e => !visited.has(e.vertex));

    nonVisited.forEach(e => {
      const newDist = distance + e.weight;
      const adjDist = distances.get(e.vertex.value);

      if (!adjDist || adjDist > newDist) {
        distances.set(e.vertex.value, newDist);
      }
    });

    nonVisited.forEach(e => heap.insert(e.vertex));
  }

  return distances.get(to.value);
}

// Demo:

const vertexA = new GenericVertex<string, number>('A');
const vertexB = new GenericVertex<string, number>('B');
const vertexC = new GenericVertex<string, number>('C');
const vertexD = new GenericVertex<string, number>('D');
const vertexE = new GenericVertex<string, number>('E');
const vertexF = new GenericVertex<string, number>('F');

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

const distance = dijkstra<string>(vertexB, vertexF);
console.log(distance);

