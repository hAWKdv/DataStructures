export class Edge<T, U> {
  constructor(
    public vertex: Vertex<T, U>,
    public weight?: U
  ) {}
}

export class Vertex<T, U> {
  edges: Edge<T, U>[] = [];

  constructor(public value: T) {}

  addAdjacentVertex(v: Vertex<T, U>, directed: boolean, weight?: U): this {
    const edge = new Edge(v, weight);
    this.edges.push(edge);

    if (!directed) {
      v.addAdjacentVertex(this, true, weight);
    }

    return this;
  }
}
