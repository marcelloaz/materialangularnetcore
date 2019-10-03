import { Component, OnInit } from '@angular/core';
import { AgendaMedicaService } from '../../services/agenda-medica.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Compromisso } from '../../model/Compromisso';
import { MatSnackBar, MatDialog, MatSnackBarRef, SimpleSnackBar } from '@angular/material';
import { EditCompromissoDialogComponent } from '../edit-compromisso-dialog/edit-compromisso-dialog.component';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss']
})
export class MainContentComponent implements OnInit {

  compromisso: Compromisso;
  constructor(
    private router: Router,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private agendaMedicaService: AgendaMedicaService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      let compromissosId = params['CompromissosId'];
      if (!compromissosId) compromissosId = 1;
      this.compromisso = null;

      console.log(compromissosId);

      this.agendaMedicaService.compromissos.subscribe(compromisso => {
        if (compromisso.length == 0) return;

        setTimeout(() => {
          this.compromisso = this.agendaMedicaService.compromissoPorId(compromissosId);
        }, 500);
      });

    });
  }

  DeleteCompromisso(compromissosId: string): void {
    console.log('excluir');
     //this.agendaMedicaService.DeleteCompromisso(compromissosId).subscribe();
  }


  EditarCompromissoDialog(compromissosId: string): void {
    let dialogRef = this.dialog.open(EditCompromissoDialogComponent, {
      width: '450px',
      data: {
        CompromissosId: compromissosId
      }
    });
  }

  openSnackBar(message: string, action: string) : MatSnackBarRef<SimpleSnackBar> {
    return this.snackBar.open(message, action, {
      duration: 5000,
    });
  }

}
