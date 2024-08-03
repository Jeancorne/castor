import { PaginationDto } from "../../../../shared/models/pagination/paginationDto";

export class FilterListEmployeesDto extends PaginationDto {
    name: string | null = null;
    identification: string | null = null;
}