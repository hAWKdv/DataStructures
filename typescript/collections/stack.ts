// Array-less implementation since array implementation is trivial

class Item<T> {
  prev: Item<T> | undefined;

  constructor(public value: T) {}
}

export class Stack<T> {
  private _tip: Item<T> | undefined;

  add(value: T): void {
    const newValue = new Item<T>(value);
    newValue.prev = this._tip;
    this._tip = newValue;
  }

  pop(): T | undefined {
    if (!this._tip) {
      return;
    }

    const val = this._tip.value;
    this._tip = this._tip.prev;

    return val;
  }

  toString(): string {
    let str = '';
    let curr = this._tip;

    while (curr) {
      str = curr.value + str;
      if (curr.prev) {
        str = ', ' + str;
      }
      curr = curr.prev;
    }

    return `[${str}]`;
  }
}

// Demo:

const stack = new Stack<number>();

stack.add(1);
stack.add(2);
stack.add(3);
stack.add(4);
stack.add(5);
console.log(stack.toString());

stack.pop();
stack.pop();
console.log(stack.toString());
