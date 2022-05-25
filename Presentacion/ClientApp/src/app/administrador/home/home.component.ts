import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent implements OnInit {

  get usuario() {
    return this.authService.usuario;
  }
  
   constructor(private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
  }

  logout() {
    this.router.navigateByUrl('/auth/login');
    this.authService.logout();
  }
}
