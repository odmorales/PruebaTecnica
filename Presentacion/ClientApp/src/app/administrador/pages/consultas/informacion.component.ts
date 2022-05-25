import { Component, OnInit } from '@angular/core';
import { EMPTY, switchMap } from 'rxjs';
import { Author } from '../../classes/author';
import { IAuthor } from '../../interfaces/author.interface';
import { IBook } from '../../interfaces/book.interface';
import { SincronizarService } from '../../services/sincronizar.service';

import Swal from 'sweetalert2';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';

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

  constructor(private sincronizarService: SincronizarService) { }

  ngOnInit(): void {

  }

  exportExcel() {
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.authors);
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
