interface Paged<T> {
  totalCount: number;
  size: number;
  pageNumber: number;
  totalPages: number;
  data: T[];
}
