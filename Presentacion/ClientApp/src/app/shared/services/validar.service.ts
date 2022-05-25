import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MyErrorStateMatcher } from '../myErrorStateMatcher';

@Injectable({
  providedIn: 'root'
})
export class ValidarService {

  miFormulario?: FormGroup;
  matcher: MyErrorStateMatcher;

  constructor() { 
    this.matcher = new MyErrorStateMatcher();
  }

  recibirFomulario(miFormulario: FormGroup) {
    this.miFormulario = miFormulario;
  }

  campoEsValido(campo:string): boolean | undefined{
    return this.miFormulario?.get(campo)?.invalid && 
      this.miFormulario?.get(campo)?.touched;
  }

  getMensaje(campo: string): string {
    const errorControl = this.miFormulario?.get(campo)?.errors;
    
    if (errorControl!['required']) return 'El campo es requerido';

    return '';
  }
}
