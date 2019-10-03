import { Component, OnInit, Inject } from '@angular/core';
import { Compromisso } from '../../model/Compromisso';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { AgendaMedicaService } from '../../services/agenda-medica.service';
import { Paciente } from '../../model/Paciente';
import { Consulta } from '../../model/Consulta';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-compromisso-dialog',
  templateUrl: './edit-compromisso-dialog.component.html',
  styleUrls: ['./edit-compromisso-dialog.component.scss']
})
export class EditCompromissoDialogComponent implements OnInit {

  compromisso: Compromisso;
  public principalForm: FormGroup;
  Compromisso: Compromisso;
  Paciente: Paciente;
  Consulta: Consulta;
  constructor(
    private router: Router,
    private dialogRef: MatDialogRef<EditCompromissoDialogComponent>,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private route: ActivatedRoute,
    private agendaMedicaService: AgendaMedicaService) {

      // this.Compromisso.CompromissosId = ;
      // console.log(this.data.CompromissosId);
     }

  ngOnInit() {
    this.compromisso = this.agendaMedicaService.compromissoPorId(this.data.CompromissosId);

    this.Compromisso = new Compromisso();
    this.Compromisso.Consulta = this.compromisso.Consulta;
    this.Compromisso.Paciente = this.compromisso.Paciente;

    this.principalForm = new FormGroup({
      nome: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      datanascimento: new FormControl(new Date()),
      datainicio: new FormControl(new Date()),
      datafim: new FormControl(new Date()),
      horainicio: new FormControl('', [Validators.required]),
      horafim: new FormControl('', [Validators.required]),
    });
  }

  public hasError = (controlName: string, errorName: string) =>{
    return this.principalForm.controls[controlName].hasError(errorName);
  }

  fechar() {
    this.dialogRef.close(null);
  }


  public atualizarCompromisso = (ownerFormValue) => {
    if (this.principalForm.valid) {
      this.agendaMedicaService.UpdateCompromisso(this.compromisso).subscribe();
      this.dialogRef.close(EditCompromissoDialogComponent);
    }
  }

}
