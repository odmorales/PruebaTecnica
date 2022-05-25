import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Author } from '../classes/author';
import { Book } from '../classes/book';
import { IAuthor } from '../interfaces/author.interface';
import { IBook } from '../interfaces/book.interface';

@Injectable({
  providedIn: 'root'
})
export class SincronizarService {

  baseApi: string = environment.baseApi;
  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getBooksApi() {
    return this.http.get<IBook[]>(`${ this.baseApi }/Books`);
  }

  getAuthorsApi() {
    return this.http.get<IAuthor[]>(`${ this.baseApi }/Authors`);
  }

  sincronizarAuthors( authors: IAuthor[] ) {
    return this.http.post<Author[]>(`${ this.baseUrl }/Author`, authors);
  }

  sincronizarBooks( books: IBook[] ) {
    return this.http.post<Book[]>(`${ this.baseUrl }/Book`, books);
  }

  get() {
    return this.http.get<Author[]>(`${ this.baseUrl }/Author`);
  }

}
