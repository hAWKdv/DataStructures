// OO representation:

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

// Formal/adjency list representation:

export class Graph<T> {
  vertices: T[];
  edges: [T, T][];

  constructor(v?: T[], e?: [T, T][]) {
    this.vertices = v || [];
    this.edges = e || [];
  }

  createVertex(value: T) {
    this.vertices.push(value);
  }

  connectVertices(from: T, to: T, directed: boolean) {
    this.edges.push([from, to]);

    if (!directed) {
      this.edges.push([to, from]);
    }
  }

  copy() {
    return new Graph<T>(
      this.vertices.slice(),
      this.edges.slice()
    );
  }
}
