import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'convertirFecha'
})
export class ConvertirFechaPipe implements PipeTransform {

  transform(fecha: string): Date{
    const date = new Date(fecha);
    
    return date;
  }

}
