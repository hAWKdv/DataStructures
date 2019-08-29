export class Heap<T> {
  private _arr: T[] = [];

  insert(value: T) {
    this._arr.push(value);
    this._balanceInsert();
  }

  extractMin(): T {
    const first = this._arr[0];

    if (first) {
      const last = this._arr.pop();
      this._arr[0] = last;
      this._balanceExtract(0);
    }

    return first;
  }

  private _balanceInsert() {
    let child = this._arr.length - 1;
    let parent = this._getParentIdx(child);

    while (this._arr[parent] > this._arr[child]) {
      const swap = this._arr[parent];
      this._arr[parent] = this._arr[child];
      this._arr[child] = swap;

      child = parent;
      parent = this._getParentIdx(child);
    }
  }

  private _balanceExtract(idx: number) {
    let smallest = idx;
    let leftChild = this._getLeftIdx(idx);
    let rightChild = this._getRightIdx(idx);

    if (this._arr[idx] > this._arr[leftChild]) {
      smallest = leftChild;
    }
    if (this._arr[idx] > this._arr[rightChild]) {
      smallest = rightChild;
    }
    if (smallest !== idx) {
      const swap = this._arr[idx];
      this._arr[idx] = this._arr[smallest];
      this._arr[smallest] = swap;
      this._balanceExtract(smallest);
    }
  }

  private _getParentIdx(i: number) {
    return Math.floor((i - 1) / 2);
  }

  private _getRightIdx(i: number) {
    return 2 * i;
  }

  private _getLeftIdx(i: number) {
    return (2 * i) + 1;
  }
}
