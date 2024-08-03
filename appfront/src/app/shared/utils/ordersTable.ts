import { NzTableQueryParams } from "ng-zorro-antd/table";

export function ordersColumns(params: NzTableQueryParams, list: any): void {
    const { sort } = params;
    const currentSort = sort.find(item => item.value !== null);
    list.sort((a: any, b: any) => (a.key == currentSort?.key) > (b.key == currentSort?.key) ? 1 : -1)
}