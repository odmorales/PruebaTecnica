import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdministradorRoutingModule } from './administrador-routing.module';
import { SincronizarComponent } from './pages/sincronizar-informacion/sincronizar/sincronizar.component';
import { InformacionComponent } from './pages/consultas/informacion.component';
import { HomeComponent } from './home/home.component';
import { MaterialModule } from '../material/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ConvertirFechaPipe } from './pipe/convertir-fecha.pipe';
import { TableModule } from 'primeng/table';
import { InputFiltroComponent } from './components/input-filtro/input-filtro.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { PrimeNgModule } from '../material/prime-ng.module';

@NgModule({
  declarations: [
    SincronizarComponent,
    InformacionComponent,
    HomeComponent,
    InputFiltroComponent,
    SpinnerComponent,
    ConvertirFechaPipe
  ],
  imports: [
    CommonModule,
    AdministradorRoutingModule,
    MaterialModule,
    PrimeNgModule,
    FlexLayoutModule,
    TableModule
  ]
})
export class AdministradorModule { }
