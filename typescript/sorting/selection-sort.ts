export function selectionSort<T>(arr: T[]) {
  for (let i = 0; i < arr.length; i += 1) {
    let min = i;

    for (let j = i + 1; j < arr.length; j += 1) {
      if (arr[min] > arr[j]) {
        min = j;
      }
    }

    const swap = arr[i];
    arr[i] = arr[min];
    arr[min] = swap;
  }
}

// Demo:

const arr = [3, 7, 6, 1, 2, 9, 1];
selectionSort<number>(arr);

console.log(arr);
