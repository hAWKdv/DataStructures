export class Node<T, U = {}> {
  parent: Node<T, U> | undefined;
  children: Node<T, U>[] = [];

  constructor(
    public value: T,
    public metadata?: U
  ) {}

  addChild(value: T) {
    const node = new Node<T, U>(value);
    node.parent = this;
    this.children.push(node);
  }
}

export class Tree<T, U = {}> {
  constructor(public root?: Node<T, U>) {}

  toString(): string {
    let str = '';
    const queue = [this.root];

    while (queue.length) {
      const node = queue.shift();
      str += node.value + ' -> ' + (node.children.map(v => v.value.toString()).join(', ') || 'NULL') + '\n';

      node.children.forEach(c => queue.push(c));
    }

    return str;
  }
}
