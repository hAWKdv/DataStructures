import { Vertex } from '../graphs/graph';

// The proper implementation should use a minimal heap instead
class VertexList<T> {
  private _arr: T[];

  constructor(arr?: T[]) {
    this._arr = arr || [];
  }

  size() {
    return this._arr.length;
  }

  push(v: T) {
    this._arr.push(v);
  }

  min(p: (v: T) => number): T {
    let min = 0;

    this._arr.forEach((v, i) => {
      if (p(v) < p(this._arr[min])) {
        min = i;
      }
    });

    const minT = this._arr[min];
    this._arr.splice(min, 1);

    return minT;
  }
}

function dijkstra<T>(from: Vertex<T, number>, to: Vertex<T, number>): number {
  const visited: Set<Vertex<T, number>> = new Set;
  const vlist: VertexList<Vertex<T, number>> = new VertexList([from]);
  const distances: Map<T, number> = new Map([[ from.value, 0 ]]);

  while (vlist.size()) {
    const v = vlist.min((vt) => distances.get(vt.value));
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

    nonVisited.forEach(e => vlist.push(e.vertex));
  }

  return distances.get(to.value);
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

const distance = dijkstra<string>(vertexB, vertexF);
console.log(distance);

