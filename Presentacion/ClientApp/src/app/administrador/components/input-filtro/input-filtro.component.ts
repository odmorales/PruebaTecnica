import { Component, Input } from '@angular/core';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-input-filtro',
  templateUrl: './input-filtro.component.html',
  styles: [
  ]
})
export class InputFiltroComponent {
  @Input() dt!: Table;

  getValue(event: any): string {
    return event.target.value;
  }
}
