import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { MatDialog, MatSnackBar, SimpleSnackBar, MatSnackBarRef } from '@angular/material';
import { Router } from '@angular/router';
import { NovoCompromissoDialogComponent } from '../novo-compromisso-dialog/novo-compromisso-dialog.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})

export class ToolbarComponent implements OnInit {

  @Output() toggleSidenav = new EventEmitter<void>();

  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private router: Router) { }

  ngOnInit() {
  }

  abrirAdicionarNovoCompromissoDialog(): void {
    let dialogRef = this.dialog.open(NovoCompromissoDialogComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('Fechou a janelinha do papai.', result);

      if (result) {
        this.openSnackBar('Operacção concluída com sucesso.', 'Clique aqui para vizualizar.')
          .onAction().subscribe(() => {
            this.router.navigate(['/agendamedica', result.CompromissosId]);
          });
      }
    });
  }

  openSnackBar(message: string, action: string) : MatSnackBarRef<SimpleSnackBar> {
    return this.snackBar.open(message, action, {
      duration: 5000,
    });
  }


}
