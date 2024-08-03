export class InfoMetaData {
    countTotal = 0;
    currentPage = 0;
    totalPages: number | null = null;
    pageSize: number | null = null;
    totalCount: number | null = null;
    hasPreviousPage: boolean | null = null;
    hasNextPage: boolean | null = null;
    previousPageUrl: string | null = null;
    nextPageUrl: string | null = null;
}