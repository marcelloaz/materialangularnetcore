import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { AgendaMedicaService } from '../../services/agenda-medica.service';
import { Compromisso } from '../../model/Compromisso';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Paciente } from '../../model/Paciente';
import { Consulta } from '../../model/Consulta';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-novo-compromisso-dialog',
  templateUrl: './novo-compromisso-dialog.component.html',
  styleUrls: ['./novo-compromisso-dialog.component.scss']
})
export class NovoCompromissoDialogComponent implements OnInit {
  public principalForm: FormGroup;
  Compromisso: Compromisso;
  Paciente: Paciente;
  Consulta: Consulta;

  constructor(
    private router: Router,
    private dialogRef: MatDialogRef<NovoCompromissoDialogComponent>,
    private agendaMedicaService: AgendaMedicaService,
    private datePipe: DatePipe) { }


  ngOnInit() {
    this.Compromisso = new Compromisso();
    this.Paciente = new Paciente();
    this.Consulta = new Consulta();

    this.Compromisso.Paciente = this.Paciente;
    this.Compromisso.Consulta = this.Consulta;

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

  salvar() {
    this.agendaMedicaService.adicionarNovoCompomissoMedico(this.Compromisso).then(compromisso => {
      this.dialogRef.close(compromisso);
    });
  }

  public novoCompromisso = (ownerFormValue) => {
    if (this.principalForm.valid) {
      this.agendaMedicaService.addCompromisso(this.Compromisso).subscribe();
      this.salvar();
    }
  }

  fechar() {
    this.dialogRef.close(null);
  }

}
