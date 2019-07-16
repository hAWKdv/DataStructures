export function insertionSort<T>(arr: T[]) {
  for (let i = 1; i < arr.length; i++) {
    for (let j = i; j > 0 && arr[j - 1] > arr[j]; j--) {
      const swap = arr[j];
      arr[j] = arr[j - 1];
      arr[j - 1] = swap;
    }
  }
}

// Demo:

const arr = [3, 7, 6, 1, 2, 9, 1];
insertionSort<number>(arr);

console.log(arr);
