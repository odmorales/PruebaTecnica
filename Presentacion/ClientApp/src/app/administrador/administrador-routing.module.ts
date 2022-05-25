import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { InformacionComponent } from './pages/consultas/informacion.component';
import { SincronizarComponent } from './pages/sincronizar-informacion/sincronizar/sincronizar.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: 'sincronizar',
        component: SincronizarComponent
      },
      {
        path: 'consultas',
        component: InformacionComponent
      },
      {
        path: '**',
        redirectTo: 'consultas'
      }
    ],
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministradorRoutingModule { }
