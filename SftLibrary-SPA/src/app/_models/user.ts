export interface User {
    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    gender: string;
    roles?: string[];
}