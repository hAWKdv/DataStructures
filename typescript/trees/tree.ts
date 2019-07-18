export class Node<T> {
  parent: Node<T> | undefined;
  children: Node<T>[] = [];

  constructor(public value: T) {}

  addChild(value: T) {
    const node = new Node(value);
    node.parent = this;
    this.children.push(node);
  }
}

export class Tree<T> {
  constructor(public root?: Node<T>) {}

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
