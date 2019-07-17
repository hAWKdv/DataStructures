// Top-down implementation with lists

function merge<T>(left: T[], right: T[]): T[] {
  const merged = [];

  let firstLeft = left[0];
  let firstRight = right[0];

  while (left.length && right.length) {
    if (firstLeft <= firstRight) {
      merged.push(firstLeft);
      left.shift();
      firstLeft = left[0];
    } else {
      merged.push(firstRight);
      right.shift();
      firstRight = right[0];
    }
  }

  while (left.length) {
    merged.push(left.shift());
  }
  while (right.length) {
    merged.push(right.shift());
  }

  return merged;
}

export function mergeSort<T>(arr: T[]): T[] {
  if (arr.length <= 1) {
    return arr;
  }

  const mid = arr.length / 2;
  let left = arr.slice(0, mid);
  let right = arr.slice(mid, arr.length);

  left = mergeSort(left);
  right = mergeSort(right);

  return merge<T>(left, right);
}

// Demo:

let arr = [3, 7, 6, 1, 2, 9, 1];
arr = mergeSort<number>(arr);

console.log(arr);
