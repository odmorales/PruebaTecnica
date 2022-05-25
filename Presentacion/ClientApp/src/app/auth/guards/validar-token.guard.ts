import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ValidarTokenGuard implements CanActivate {
  
  constructor(private authService: AuthService,
    private router: Router) {}

canActivate(route: ActivatedRouteSnapshot): Observable<boolean> | boolean {

  const usuario = this.authService.usuario;

  return this.authService.validarToken()
    .pipe(
      tap(valid => {
        if( !valid ){
          this.router.navigateByUrl('/auth');
        }        
      })
    )

}
canLoad(): Observable<boolean> | boolean {

  return this.authService.validarToken()
    .pipe(
      tap(valid => {
        if( !valid ){
          this.router.navigateByUrl('/auth');
        }
      })
    )
}
  
}
