import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { AgendamedicaAppComponent } from './agendamedica-app.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { MainContentComponent } from './components/main-content/main-content.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from '../../../src/shared/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AgendaMedicaService } from './services/agenda-medica.service';
import { HttpClientModule } from '@angular/common/http';
import { NovoCompromissoDialogComponent } from './components/novo-compromisso-dialog/novo-compromisso-dialog.component';
import { EditCompromissoDialogComponent } from './components/edit-compromisso-dialog/edit-compromisso-dialog.component';



const routes: Routes = [
  {
    path: '', component: AgendamedicaAppComponent,
    children: [
      { path: ':CompromissosId', component: MainContentComponent },
      { path: '', component: MainContentComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];


@NgModule({
  declarations: [
    AgendamedicaAppComponent,
     ToolbarComponent,
     SidenavComponent,
     MainContentComponent,
     NovoCompromissoDialogComponent,
     EditCompromissoDialogComponent
    ],
    providers: [
      DatePipe,
      AgendaMedicaService
    ],
  imports: [
    CommonModule,
    HttpClientModule,
    MaterialModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  entryComponents: [
    NovoCompromissoDialogComponent,
    EditCompromissoDialogComponent
  ]
})
export class AgendamedicaModule { }
