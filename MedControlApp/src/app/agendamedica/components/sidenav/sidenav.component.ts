
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';
import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { Compromisso } from '../../model/Compromisso';
import { AgendaMedicaService } from '../../services/agenda-medica.service';

const SMALL_WIDTH_BREAKPOINT = 720;

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  private mediaMatcher: MediaQueryList =
           matchMedia(`(max-width: ${SMALL_WIDTH_BREAKPOINT}px)`);

   dir: string = 'ltr';
   compromissos: Observable<Compromisso[]>;

  constructor(
    zone: NgZone,
    private agendaMedicaService: AgendaMedicaService,
    private router: Router) {
    this.mediaMatcher.addListener(mql =>
      zone.run(() => matchMedia(`(max-width: ${SMALL_WIDTH_BREAKPOINT}px)`)));

  }

  @ViewChild(MatSidenav, {static: false}) sidenav: MatSidenav;

  ngOnInit() {
    this.compromissos = this.agendaMedicaService.compromissos;
    this.agendaMedicaService.carregarTodosCompromissos();
    this.router.events.subscribe(() => {
      if (this.isScreenSmall())
        this.sidenav.close();
    })
  }

  toggleDir() {
    this.dir = this.dir == 'ltr' ? 'rtl' : 'ltr';
    this.sidenav.toggle().then(() => this.sidenav.toggle());
  }

  isScreenSmall(): boolean {
    return this.mediaMatcher.matches;
  }

}
