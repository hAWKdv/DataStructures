import { Vertex } from '../graph';

function BFS<T, U>(start: Vertex<T, U>, cb: (v: Vertex<T, U>) => void) {
  const queue = [start];
  const visited: Set<Vertex<T, U>> = new Set();

  while (queue.length) {
    const v = queue.shift();

    if (!visited.has(v)) {
      cb(v);
      visited.add(v);
      v.edges.forEach(e => queue.push(e.vertex));
    }

  }
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

BFS<string, number>(vertexB, (v: Vertex<string, number>) => console.log(v.value));
