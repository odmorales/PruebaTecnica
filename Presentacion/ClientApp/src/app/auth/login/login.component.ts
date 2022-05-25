import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidarService } from 'src/app/shared/services/validar.service';
import { Usuario } from '../interfaces/usuario.interface';
import { AuthService } from '../services/auth.service';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent implements OnInit {

  usuarios: Usuario[] = []
  usuario?: Usuario;

  miFormulario: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  })

  constructor(private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    public validarService: ValidarService) {
      this.validarService.recibirFomulario(this.miFormulario);
    }


  ngOnInit(): void {
    this.authService.usuarios().subscribe(users => {
      this.usuarios = users;
      console.log(this.usuarios);
    });
  }

  login() {

    if (this.miFormulario.invalid) {
      this.miFormulario.markAllAsTouched();
      return;
    }

    const { username, password } = this.miFormulario.value;

    this.usuarios.forEach(user => {
      if (username.replace(/\s+/g, '').toUpperCase() === user.userName.replace(/\s+/g, '').toUpperCase()) {
        this.usuario = user;
      }
    });

    if (this.usuario?.userName.replace(/\s+/g, '').toUpperCase() === username.replace(/\s+/g, '').toUpperCase() 
          && this.usuario?.password === password) {
      this.authService.login(this.usuario!).subscribe( user =>{
        this.router.navigate(['/user/consultas']);
      });
    }else {
      Swal.fire('Error', 'Usuario o contraseña incorrectos', 'error');
    }
  }

}
