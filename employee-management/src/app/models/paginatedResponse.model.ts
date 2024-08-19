import { Employee } from "./employee.model";

export class PaginatedResponse{
    data: Employee[] = [];
    totalCount!: number;
    pageNumber!: number;
    pageSize!: number;
}