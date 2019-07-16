// We will use an array as if it isn't associative

export class HashMap<T> {
  private _arr: T[] = [];

  set(key: string, val: T): void {
    const hash = this._hash(key);
    this._arr[hash] = val;
  }

  get(key: string): T | undefined {
    const hash = this._hash(key);
    return this._arr[hash];
  }

  // This is not a proper has function! It may result in collisions easily
  private _hash(key: string): number {
    return new Array(key.length)
      .fill(0)
      .map((_, idx: number) => key.charCodeAt(idx))
      .reduce((accum: number, curr: number) => accum += curr >> 2, 0);
  }

  // Shows the indices
  toString(): string {
    let values = [];

    for (let i = 0; i < this._arr.length; i+=1) {
      if (this._arr[i]) {
        values.push(`[${i}: ${this._arr[i]}]`);
      }
    }

    return `[${values.join(', ')}]`;
  }
}

// Demo:

const map = new HashMap<string | number>();
map.set('name', 'Georgi');
map.set('age', 1337);
map.set('github', 'hawkgs');

console.log(map.get('name'));
console.log(map.get('age'));
console.log(map.get('github'));

console.log(map.toString());
