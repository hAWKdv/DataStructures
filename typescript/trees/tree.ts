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

  toString(): string {
    return this._printNode(this.root);
  }

  private _printNode(node: Node<T>): string {
    let str = node.value.toString();

    node.children.forEach((c: Node<T>) => {
      str += this._printNode(c) + ' ';
    })

    return `(${str})`;
  }
}
