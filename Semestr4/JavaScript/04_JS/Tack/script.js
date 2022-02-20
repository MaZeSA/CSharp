let header =
[{tag: 'header',
childs:[{tag: 'div>class=collapse bg-dark@id=navbarHeader',
        childs:[{tag: 'div>class=container',
                childs: [{tag: 'div>class=row',
                        childs: [{tag: 'div>class=col-sm-8 col-md-7 py-4',
                                 childs:[{tag: 'h4>class=text-white>About', 
                                        childs:[]},
                                       {tag: 'p>class=text-muted>Add some information about the album below, the author, or any other background context. Make it a few sentences long so folks can pick up some informative tidbits. Then, link them off to some social networking sites or contact information.',
                                        childs:[]}
                                      ]
                              },
                              {tag: 'div>class=col-sm-4 offset-md-1 py-4',
                               childs:[{tag:'h4>class=text-white>Contact', 
                                       childs: []},
                                      {tag: 'ul>class=list-unstyled',
                                      childs:[{tag: 'li', 
                                             childs: [{ tag: 'a>href=#@lass=text-white>Follow on Twitter', childs:[]}]},
                                             {tag: 'li',
                                              childs: [{tag:'a>href=#@class=text-white>Like on Facebook', childs:[]}]},
                                             {tag: 'li',
                                             childs:[{tag:'a>href=#@class=text-white>Email me', childs:[]}]}
                                             ]
                                      }]
                               }]
                         }]
            }]
        },
        {tag:'div>class=navbar navbar-dark bg-dark shadow-sm',
        childs:[{tag:'div>class=container', 
                childs:[{tag:'a>href=#@class=navbar-brand d-flex align-items-center',
                        childs:[{tag:'svg>xmlns=http://www.w3.org/2000/svg@width=20@height=20@fill=none@stroke=currentColor@stroke-linecap=round@stroke-linejoin=round@stroke-width=2@aria-hidden=true@class=me-2@viewBox=0 0 24 24',
                                childs:[{tag:'path>d=M23 19a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h4l2-3h6l2 3h4a2 2 0 0 1 2 2z',
                                        childs:[]
                                        },
                                        {tag:'circle>cx=12@cy=13@r=4',
                                        childs:[]
                                        }]
                                },
                                {tag:'strong>>Album',
                                childs:[]
                                }
                                ]
                             },
                        {tag:'button>class=navbar-toggler@type=button@data-bs-toggle=collapse@data-bs-target=#navbarHeader@aria-controls=navbarHeader@aria-expanded=false@aria-label=Toggle navigation',
                        childs:[{tag:'span>class=navbar-toggler-icon',
                                childs:[]
                                }]
                        }]
                }] 
       }]
}];

const main = [{tag: 'main', 
                    childs:[{tag:'section>class=py-5 text-center container',
                            childs:[{tag:'div>class=row py-lg-5',
                                    childs:[{tag:'div>class=col-lg-6 col-md-8 mx-auto',
                                            childs:[{tag:'h1>class=fw-light>Album example',
                                                    childs:[]},
                                                    {tag:'p>class=lead text-muted>Something short and leading about the collection below—its contents, the creator, etc. Make it short and sweet, but not too short so folks don’t simply skip over it entirely.',
                                                    childs:[]},
                                                    {tag:'p',
                                                    childs:[{tag:'a>href=#@class=btn btn-primary my-2>Main call to action',
                                                            childs:[]},
                                                            {tag:'a>href=#@class=btn btn-secondary my-2>Secondary action',
                                                           childs:[]}
                                                            ]}
                                                    ]}
                                            ]}
                                    ]}
                            ]},
                            {tag:'div>class=album py-5 bg-light', 
                             childs:[{tag:'div>class=container',
                                    childs:[{tag: 'div>id=alboumItems@class=row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3',
                                             childs:[]}
                                    ]}
                            ]}
             ];

const item =[{tag:'div>class=col',
              childs:[{tag:'div>class=card shadow-sm',
                      childs:[{tag:'svg>class=bd-placeholder-img card-img-top@width=100%@height=225@xmlns=http://www.w3.org/2000/svg@role=img@aria-label=Placeholder: Thumbnail@preserveAspectRatio=xMidYMid slice@focusable=false',
                              childs:[{tag:'title>>Placeholder', childs:[]},
                                      {tag:'rect>width=100%@height=100%@fill=#55595c', childs:[]},
                                      {tag:'text>x=50%@y=50%@fill=#eceeef@dy=.3em>Thumbnail', childs:[]}]
                              },
                              {tag:'div>class=card-body',
                              childs:[{tag:'p>class=card-text>This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
                                       childs:[]},
                                       {tag:'div>class=d-flex justify-content-between align-items-center',
                                        childs:[{tag:'div>class=btn-group',
                                                 childs:[{tag:'button>type=button@class=btn btn-sm btn-outline-secondary>View',childs:[]},
                                                         {tag:'button>type=button@class=btn btn-sm btn-outline-secondary>Edit', childs:[]}
                                                ]}

                                        ]},
                                        {tag:'small>class=text-muted>9 mins', childs:[]}
                                    ]}
                            ]
                    }]
            }

];


let body = document.querySelector(".root"); 

function CreateTag(parent, newTag){
    
    let tag;
    let newEl;
    newTag.forEach(element => {
        tag = element.tag.split('>');
       
        newEl = document.createElement(tag[0]);
        parent.appendChild(newEl);

        if(tag.length > 1) {
            CreateAttributes(newEl, tag[1]);
        }
        if(element.childs.length > 0){
            CreateTag(newEl, element.childs);
        }
        if(tag.length>2){
            newEl.innerText=tag[2];
        }

    });
}

function CreateAttributes(parent, attributes)
{  
    if(attributes.length <= 0) return;
   
    let attr = attributes.split('@');
  
    let at;
    attr.forEach(element=>{
        at = element.split('=');
        parent.setAttribute(at[0], at[1]);
    })
}

CreateTag(body, header);
CreateTag(body, main);
const alb = document.querySelector("#alboumItems"); 
for (var i = 0; i < 9; i++) {
    CreateTag(alb, item);
  }

console.log(header);