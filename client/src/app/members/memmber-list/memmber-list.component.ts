import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-memmber-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './memmber-list.component.html',
  styleUrl: './memmber-list.component.css'
})
export class MemmberListComponent implements OnInit {

  memberService = inject(MembersService);

  ngOnInit(): void{

    if(this.memberService.members().length===0)
    this.loadMembers();
  }

  loadMembers(){
    this.memberService.getMembers()
  }
}
