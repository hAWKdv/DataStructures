export class Vertex<T, U> {
  edges: Set<Edge<T, U>> = new Set();

  constructor(public value: T) {}

  addEdge(edge: Edge<T, U>) {
    this.edges.add(edge);
  }
}

export class Edge<T, U> {
  constructor(
    public start: Vertex<T, U>,
    public end: Vertex<T, U>,
    public weight?: U
  ) {}
}

export class Graph<T, U> {
  vertices: Set<Vertex<T, U>> = new Set();
  edges: Set<Edge<T, U>> = new Set();

  addEdge(from: Vertex<T, U>, to: Vertex<T, U>, weight?: U) {
    if (!this.vertices.has(from)) {
      this.vertices.add(from);
    }
    if (!this.vertices.has(to)) {
      this.vertices.add(to);
    }

    const edge = new Edge<T, U>(from, to, weight);
    this.edges.add(edge);
  }
}
