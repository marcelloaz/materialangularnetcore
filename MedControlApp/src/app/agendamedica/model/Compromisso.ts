import { Paciente } from './Paciente';
import { Consulta } from './Consulta';

export class Compromisso {
  CompromissosId: number;
  Paciente: Paciente;
  Consulta: Consulta;
  Observacao: string;
}
