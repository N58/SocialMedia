interface Paged<T> {
  count: number;
  totalCount: number;
  totalPages: number;
  page: number;
  size: number;
  data: T[];
}
