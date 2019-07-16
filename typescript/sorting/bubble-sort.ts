export function bubbleSort<T>(arr: T[]) {
  for (let i = arr.length; i >= 0; i -= 1) {
    for (let j = 0; j < i - 1; j += 1) {
      if (arr[j] > arr[j + 1]) {
        const swap = arr[j + 1];
        arr[j + 1] = arr[j];
        arr[j] = swap;
      }
    }
  }
}

// Demo:

const arr = [3, 7, 6, 1, 2, 9, 1];
bubbleSort<number>(arr);

console.log(arr);
