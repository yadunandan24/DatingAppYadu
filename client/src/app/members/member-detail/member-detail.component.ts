import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  standalone: true,  //added coz photo gallery is also standalone and comp has to be standalone to use that
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  imports: [CommonModule,TabsModule, GalleryModule]  //added due to standaloen lost access to ngif etc
})
export class MemberDetailComponent implements OnInit {

  member: Member | undefined;
  images: GalleryItem[] =[];

  constructor(private memberSerivce: MembersService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    const username = this.route.snapshot.paramMap.get('username');
    if(!username) return;
    this.memberSerivce.getMember(username).subscribe({
      next: member => {
        this.member = member;
        this.getImages();
      }
    })
  }

  getImages(){
    if(!this.member) return;
    for(const photo of this.member?.photos)
    { 
      this.images.push(new ImageItem({src:photo.url, thumb:photo.url}))
    }
  }

}
