import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Compromisso } from '../model/Compromisso';
import { resolve } from 'q';
import { environment } from 'src/environments/environment';

import { DatePipe } from '@angular/common';
var httpOptions = {headers: new HttpHeaders({"Content-Type": "application/json"})};
@Injectable()
export class AgendaMedicaService {
  apiUrl = environment.apiUrl;
  private _compromissos: BehaviorSubject<Compromisso[]>;

  private dataStore: {
    compromissos: Compromisso[]
  }

  constructor(private http: HttpClient, private datePipe: DatePipe) {
    this.dataStore = { compromissos: [] };
    this._compromissos = new BehaviorSubject<Compromisso[]>([]);
  }

  get compromissos(): Observable<Compromisso[]> {
    return this._compromissos.asObservable();
  }

  compromissoPorId(id: number){
    return this.dataStore.compromissos.find(x => x.CompromissosId == id);
  }

  carregarTodosCompromissos() {
    const usersUrl = 'http://localhost:50075/api/compromissos';

    return this.http.get<Compromisso[]>(usersUrl)
      .subscribe(data => {
        this.dataStore.compromissos = data;
        this._compromissos.next(Object.assign({}, this.dataStore).compromissos);
      }, error => {
        console.log('Falha ao carregar a agenda.');
      });
  }

  adicionarNovoCompomissoMedico(compromisso: Compromisso): Promise<Compromisso> {
    return new Promise((resolver, reject) => {
      compromisso.CompromissosId = this.dataStore.compromissos.length + 1;
      compromisso = this.setObservacao(compromisso);
      this.dataStore.compromissos.push(compromisso);
      this._compromissos.next(Object.assign({}, this.dataStore).compromissos);
      resolver(compromisso);
    });
  }

  addCompromisso(AddCompromisso: Compromisso): Observable<Compromisso> {
    AddCompromisso = this.setObservacao(AddCompromisso);
    return this.http.post<Compromisso>(`${this.apiUrl}/compromissos`, AddCompromisso);
}

UpdateCompromisso(UpdateCompromisso: Compromisso): Observable<Compromisso> {

  return this.http.put<Compromisso>(`${this.apiUrl}/compromissos`, UpdateCompromisso);
  }

  setObservacao(compromisso: Compromisso): Compromisso
  {

    var dtFinal = this.datePipe.transform(compromisso.Consulta.DataHoraFinal,"yyyy-MM-dd");
    var dtInicio = this.datePipe.transform(compromisso.Consulta.DataHoraIncial,"yyyy-MM-dd");
    var dataInicialFormat = dtInicio + 'T' + compromisso.Consulta.HoraInicio + ':00';
    var dataFinalFormat = dtFinal + 'T' + compromisso.Consulta.HoraFinal + ':00';

    compromisso.Consulta.DataHoraIncial = new Date(dataInicialFormat);
    compromisso.Consulta.DataHoraFinal = new Date(dataFinalFormat);

       var observacao = " Data hora inicio tratamento " +
                          compromisso.Consulta.DataHoraIncial +
                         " e Data hora fim tratamento " +  compromisso.Consulta.DataHoraFinal;

       compromisso.Observacao = observacao;
       console.log(compromisso.Observacao);
       return compromisso;
  }
}
