import { Book } from "./book";

export class Author {

    key!: number;
    id!: number;
    idBook!: number;
    firstName?: string;
    lastName?: string;
    book: Book = new Book();
}