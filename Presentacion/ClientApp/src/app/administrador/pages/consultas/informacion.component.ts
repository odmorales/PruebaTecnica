import { Component, OnInit } from '@angular/core';
import { EMPTY, switchMap } from 'rxjs';
import { Author } from '../../classes/author';
import { IAuthor } from '../../interfaces/author.interface';
import { IBook } from '../../interfaces/book.interface';
import { SincronizarService } from '../../services/sincronizar.service';

import Swal from 'sweetalert2';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';
import { Excel } from '../../interfaces/excel.interface';
import { AuthorMap } from '../../classes/authorMap';

const EXCEL_TYPE = 
  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet; charset=UTF-8";
const EXCEL_EXT = ".xlsx";

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
  authorsMap: AuthorMap[] = [];
  author: AuthorMap;

  constructor(private sincronizarService: SincronizarService) { 
    this.author = {
      nombre: "",
      date: "",
      libro: ""
    }
  }

  ngOnInit(): void {

  }

  mapearAuthors() {

    this.authors.forEach(author => {

      this.author = new AuthorMap(
        author.firstName!,
        author.book.publishDate!,
        author.book.title!
      );

      this.authorsMap = [...this.authorsMap,this.author];
    });

    console.log(this.authorsMap);
  }

  exportExcel() {

    this.mapearAuthors();

    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.authorsMap);
    const workbook: XLSX.WorkBook = { Sheets: {'data': worksheet},
    SheetNames:['data']};
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type:'array' });
    //Guardar archivo
    this.saveAsExcel(excelBuffer, "authors");
  }

  private saveAsExcel(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {type: EXCEL_TYPE});
    FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXT);
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
