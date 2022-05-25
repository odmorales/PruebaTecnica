import { Component, OnInit } from '@angular/core';
import { EMPTY, switchMap } from 'rxjs';
import { Author } from '../../classes/author';
import { IAuthor } from '../../interfaces/author.interface';
import { IBook } from '../../interfaces/book.interface';
import { SincronizarService } from '../../services/sincronizar.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-informacion',
  templateUrl: './informacion.component.html',
  styles: [
  ]
})
export class InformacionComponent implements OnInit {

  iBooks: IBook[]  = [];
  iAuthors: IAuthor[] = [];
  authors: Author[] = [];


  constructor(private sincronizarService: SincronizarService) { }

  ngOnInit(): void {
  
  }

  sincronizar() {

    this.sincronizarService.getBooksApi().pipe(
      switchMap( (result) => result ? this.sincronizarService.sincronizarBooks(result): EMPTY)
    ).subscribe( (_) => {
      this.sincronizarService.getAuthorsApi().pipe(
        switchMap( (result) => result ? this.sincronizarService.sincronizarAuthors(result): EMPTY)
      ).subscribe( (_) => {
        this.alerta();
        this.getAuthors();
      });
    });

  }

  getAuthors() {
    this.sincronizarService.get().subscribe( authors => {
      this.authors = authors;
    });
  }

  alerta() {
    Swal.fire({
      position: 'center',
      icon: 'success',
      title: 'Sincronizacion completada',
      showConfirmButton: false,
      timer: 1500
    })
  }
}
