export class Node<T> {
  children: Node<T>[] = [];

  constructor(public value: T) {}

  addChild(value: T) {
    const node = new Node(value);
    this.children.push(node);
  }
}

export class Tree<T> {
  constructor(public root?: Node<T>) {}
}
