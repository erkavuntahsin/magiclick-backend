örnek bir sass dosyası (mercedes için yazılan projeden bir kısım)

@import "../../../../assets/stylesheets/variables";
.main {
    background-image: radial-gradient(circle at 21% 56%, #ececec, #ffffff 94%);
}

.icon-print {
    width: 32px;
    height: 25px;
}

.icon-share {
    width: 23px;
    height: 25px;
}

.icon-pdf {
    width: 20px;
    height: 25px;
}

.icon-whatsapp {
    width: 25px;
    height: 25px;
}

.icon-mail {
    width: 25px;
    height: 17px;
}

.icon-sms {
    width: 22px;
    height: 20px;
}

.icon-copy {
    width: 24px;
    height: 24px;
}

.icon-fuel {
    width: 21px;
    height: 20px;
}

.icon-engine1 {
    width: 27px;
    height: 20px;
}

.icon-engine2 {
    width: 27px;
    height: 20px;
}

span.pull-left-md {
    float: left;
}

.f-18 {
    font-size: 18px;
    margin-left: -20px;
    white-space: nowrap;
}

.txt-demi {
    font-family: DaimlerCS-Demi;
}

.size-18 {
    font-size: 18px;
}

.title {
    font-family: DaimlerCAC;
    font-size: 28px;
    font-weight: normal;
    font-stretch: normal;
    font-style: normal;
    line-height: normal;
    letter-spacing: normal;
    color: $font-color;
}

.icon-bookmark {
    height: 35px;
}

.product-image {
    height: auto;
    width: 100%;
}

.card-header:hover {
    & .acc-headers:nth-child(1),
    & .acc-headers:nth-child(2) {
        & span {
            color: #00adef;
        }
    }
}

.appearance-bar{
    height: 60px;
    border-radius: 1px;
    background-color: #3c3c3c;

    img{
        height: 30px;
    }
}

.subTitle {
    font-family: DaimlerCAC;
    font-size: 24px;
    font-weight: normal;
    font-stretch: normal;
    font-style: normal;
    line-height: normal;
    letter-spacing: normal;
    color: #ffffff;
}

.card-header.collapsed {
    & .acc-headers:nth-child(1),
    & .acc-headers:nth-child(2) {
        color: #ffffff !important;
    }
    & .after-arrow:after {
        content: url("../../../../assets/images/arrow_2.png");
        position: absolute;
        right: 20px;
        top: 29px;
        width: 5px;
        height: 10px;
        clear: both;
        color: #ffffff !important;
    }
}

.card-header {
    & .acc-headers:nth-child(1),
    & .acc-headers:nth-child(2) {
        color: #00acf2 !important;
    }
    & .after-arrow:after {
        content: url("../../../../assets/images/arrow.png");
        position: absolute;
        right: 20px;
        top: 29px;
        width: 5px;
        height: 10px;
        clear: both;
    }
}

.year {
    font-family: DaimlerCS;
    font-size: 14px;
    font-weight: normal;
    font-stretch: normal;
    font-style: normal;
    line-height: normal;
    letter-spacing: normal;
    color: $font-color;
    margin-bottom: 8px;
}

.numbers {
    padding: 5px;
    font-family: DaimlerCS;
    font-size: 18px;
    font-weight: 300;
    font-stretch: normal;
    font-style: normal;
    line-height: normal;
    letter-spacing: normal;
    color: $font-color;
    cursor: pointer;
    &.active {
        font-family: DaimlerCS-Demi;
        font-size: 18px;
        font-weight: normal;
        font-stretch: normal;
        font-style: normal;
        line-height: normal;
        letter-spacing: normal;
        color: #00adef;
    }
}

.br-split {
    border-right: 1px solid $font-color;
}

.info {
    font-size: 14px;
    font-weight: normal;
    font-stretch: normal;
    font-style: normal;
    line-height: normal;
    letter-spacing: normal;
    text-align: right;
    color: $font-color;
    font-family: DaimlerCS;
    padding: 0;
    margin-top: 10px;
}

.info-share {
    font-size: 12px;
    font-weight: normal;
    font-stretch: normal;
    font-style: normal;
    line-height: normal;
    letter-spacing: normal;
    text-align: right;
    color: $font-color;
    font-family: DaimlerCS;
    padding: 0;
    margin: 0;
    width: 100%;
    text-align: center;
}

a {
    cursor: pointer;
}

.share-div {
    width: 272px;
    height: 100.8px;
    border-radius: 1px;
    box-shadow: 0 10px 20px 0 rgba(0, 0, 0, 0.3);
    background-color: #ffffff;
    position: absolute;
    top: 97%;
    left: 50%;
    display: none !important;
    z-index: 9;
    &:after {
        bottom: 100%;
        left: 73%;
        border: solid transparent;
        content: " ";
        height: 0;
        width: 0;
        position: absolute;
        pointer-events: none;
    }
    &:after {
        border-color: rgba(136, 183, 213, 0);
        border-bottom-color: #ffffff;
        border-width: 10px;
    }
}

.active-flex {
    display: flex !important;
}

.Daimler-CS {
    font-family: DaimlerCs;
}

.clr-dark {
    font-family: DaimlerCS;
    background-color: $primary-color;
    color: #ffffff;
    & .card-header {
        background-image: linear-gradient(to bottom, #3c3c3c, #1f1f1f);
        padding: 1% 1% 1% 1%;
        cursor: pointer;
        & .acc-headers {
            font-size: 14px;
            &:first-child {
                font-family: DaimlerCAC;
            }
            &:nth-child(1),
            &:nth-child(2) {
                font-size: 18px;
            }
            &.active {
                height: 18px;
                font-family: DaimlerCS;
                font-size: 18px;
                font-weight: 300;
                font-stretch: normal;
                font-style: normal;
                line-height: normal;
                letter-spacing: normal;
                color: #00adef;
            }
            font-size: 14px;
            font-weight: 300;
            font-stretch: normal;
            font-style: normal;
            line-height: normal;
            letter-spacing: normal;
            color: #ffffff;
            float: left;
            & .split-right {
                margin-left: 4px;
            }
            & .split-right.fuel:after {
                right: 4%;
                content: " ";
                width: 1px;
                position: absolute;
                pointer-events: none;
                height: 30px;
                background-color: #000000;
                top: -20%;
            }
            & .split-right.engine:after {
                right: 18%;
                content: " ";
                width: 1px;
                position: absolute;
                pointer-events: none;
                height: 30px;
                background-color: #000000;
                top: -20%;
            }
            & .split-right.torque:after {
                right: 17%;
                content: " ";
                width: 1px;
                position: absolute;
                pointer-events: none;
                height: 30px;
                background-color: #000000;
                top: -20%;
            }
        }
    }
    & .card {
        border: none;
        font-weight: normal;
        font-stretch: normal;
        font-style: normal;
        line-height: normal;
        letter-spacing: normal;
        color: #ffffff;
        background-color: transparent;
    }
    & .card-body {
        background-color: #333333;
        font-family: DaimlerCS;
        font-size: 16px;
        font-weight: normal;
        font-stretch: normal;
        font-style: normal;
        line-height: normal;
        letter-spacing: normal;
        color: #ffffff;
        & .right-desc {
            padding: 2% 0 2% 0;
            margin-right: 3rem;
        }
    }
    & .accordion-sub-buttons {
        & .acc-headers {
            text-align: center;
            font-size: 14px;
            font-weight: 300;
            font-stretch: normal;
            font-style: normal;
            line-height: normal;
            letter-spacing: normal;
            font-family: DaimlerCS;
            color: #ffffff;
            padding: 1.5% 0 1.5% 0;
            float: left;
            background-image: linear-gradient(to bottom, #666666, #1f1f1f);
            border-right: 1px solid #000000;
            & .split-right {
                margin-left: 11px;
            }
            & img {
                width: 20px;
                height: 20px;
            }
        }
    }
    .border-bottom {
        border-bottom: 1px solid #ffffff;
    }
    .slogan {
        font-family: DaimlerCAC;
        font-size: 30px;
        font-weight: normal;
        font-stretch: normal;
        font-style: normal;
        line-height: normal;
        letter-spacing: normal;
        color: #ffffff;
    }
    .tiles {
        cursor: pointer;
        &:hover {
            background-image: linear-gradient(to bottom, #1f1f1f, #1f1f1f);
        }
        border-radius: 1px;
        background-image: linear-gradient(to bottom,
        #3c3c3c,
        #1f1f1f);
        width: 23.333333%;
        display: inline-block;
        & a {
            display: block;
            padding: 9% 0%;
            font-family: DaimlerCS;
            font-size: 22px;
            font-weight: 300;
            font-stretch: normal;
            font-style: normal;
            line-height: normal;
            letter-spacing: normal;
            color: #ffffff;
            & img {
                min-height: 52px;
                max-height: 52px;
            }
            & .right-arrow {
                content: "";
                border: solid #ffffff;
                border-width: 0 3px 3px 0;
                display: inline-block;
                padding: 3px;
                transform: rotate(-45deg);
                -webkit-transform: rotate(-45deg);
                margin-left: 10px;
                margin-top: 3%;
                float: right;
            }
            & div {
                display: inline-block;
            }
        }
    }
    .information {
        font-family: DaimlerCS;
        font-size: 14px;
        font-weight: 300;
        font-stretch: normal;
        font-style: normal;
        line-height: normal;
        letter-spacing: normal;
        color: #999999;
    }
}

.incoming-main {
    & .incoming {
        & .text {
            opacity: 0.5;
            font-family: DaimlerCAC;
            font-size: 24px;
            font-weight: normal;
            font-stretch: normal;
            font-style: normal;
            line-height: normal;
            letter-spacing: normal;
            color: #333333;
        }
        & .incoming-span {
            font-family: DaimlerCS;
            font-size: 18px;
            font-weight: bold;
            font-stretch: normal;
            font-style: normal;
            line-height: normal;
            letter-spacing: normal;
            color: #333333;
        }
        & .incoming-span-blur {
            font-family: DaimlerCS;
            font-size: 18px;
            font-weight: normal;
            font-stretch: normal;
            font-style: normal;
            line-height: normal;
            letter-spacing: normal;
            color: #333333;
        }
    }
}

@media (max-width: $breakpoint-mobile) {
    .mobile-pb {
        padding-bottom: 12px !important;
        margin-bottom: unset !important;
    }
    .custom-p {
        padding: 10px 5px 10px 5px;
        &:last-child {
            padding-bottom: 0px !important;
        }
    }
    span.pull-left-md {
        float: unset !important;
    }
    .icon-bookmark {
        width: 22px !important;
        height: 28px !important;
    }
    .accordion-sub-buttons {
        & .acc-headers {
            padding: 4% 0% 4% 0% !important;
            margin-top: 1px !important;
        }
    }
    span.title {
        padding-top: 0 !important;
        padding-left: 20px !important;
    }
    .m-order-first {
        order: 1 !important;
        padding-right: unset !important;
        padding-left: unset !important;
    }
    .m-order-second {
        order: 2 !important;
    }
    .m-order-third {
        order: 2 !important;
    }
    .mp-0 {
        padding: 0 0 0 0 !important;
    }
    .card-body {
        background: transparent !important;
        padding-left: 30px !important;
        padding-right: 27px !important;
        padding-bottom: 0 !important;
    }
    .card-header {
        padding-left: 1rem !important;
        padding-right: 1rem !important;
        & .split-right.fuel:after {
            right: 16% !important;
            height: 21px !important;
            top: 0% !important;
        }
        & .split-right.engine:after {
            right: 5% !important;
            height: 22px !important;
            top: 0% !important;
        }
        & .split-right.torque:after {
            display: none !important;
        }
        & .acc-headers {
            padding-left: 0 !important;
            padding-right: 0 !important;
            &:not(.advice-price) {
                padding-bottom: 1rem !important;
            }
            &:first-child {
                white-space: nowrap !important;
                padding-bottom: 0 !important;
            }
            &:nth-child(2),
            &:nth-child(5) {
                text-align: center !important;
            }
        }
    }
    .tiles {
        margin-bottom: 2px !important;
        margin-right: 0 !important;
        width: 100% !important;
        & a {
            font-size: 18px !important;
            padding: 5% 0% !important;
            & .brochure {
                max-height: 30px !important;
                min-height: 30px !important;
                max-width: 21px !important;
            }
            & .configure {
                max-width: 32px !important;
                max-height: 26px !important;
                min-height: 26px !important;
            }
            & .drive {
                max-width: 30px !important;
                max-height: 30px !important;
                min-height: 30px !important;
            }
            & .car-large {
                max-width: 27.6px !important;
                max-height: 24.2px !important;
                min-height: 24.2px !important;
            }
        }
    }
    .card-header.collapsed {
        & .after-arrow:after {
            right: 20px;
            top: 50px;
        }
    }
    .card-header {
        & .after-arrow:after {
            right: 20px;
            top: 50px;
        }
    }
}

.showroomOverlay{
    justify-content: center;
    align-items: center;
    text-align: center;
    display: none !important;
    position: fixed;
    width: 100%;
    height: 100vh;
    z-index: 99999; 
    background-color: rgba(0, 0, 0, 0.9);
    top: 0;
    left: 0;
    
    .showroomContainer{
        width: 80%;
        min-height: 600px;

        div {
            iframe {
                width: 100% !important;
                min-height: 600px;
                overflow: hidden !important;
            }
        }

        .frame-header{
            width: 100%; 
            padding-left: 8px; 
            padding-right: 8px;
            padding-bottom: 20px;

        }

        .frame-title{
            font-family: DaimlerCAC;
            font-size: 24px;
            font-weight: normal;
            font-stretch: normal;
            font-style: normal;
            line-height: normal;
            letter-spacing: normal;
            color: #ffffff;
        }

        .iframeClose{
            padding-left: 10px;
            padding-right: 5px;
            font-weight: 900;
            font-size: 24px;
          &:hover{
              cursor: pointer;
          }
        }
    }
}

.pb-10 {
    padding-bottom: 10%;
}

@media (max-width: $breakpoint-tablet) {
    .mobile-pb {
        padding-bottom: 12px !important;
        margin-bottom: unset !important;
    }
    .custom-p {
        padding: 10px 5px 10px 5px;
        &:last-child {
            padding-bottom: 0px !important;
        }
    }
    span.pull-left-md {
        float: unset !important;
    }
    .icon-bookmark {
        width: 22px !important;
        height: 28px !important;
    }
    .accordion-sub-buttons {
        & .acc-headers {
            padding: 4% 0% 4% 0% !important;
            margin-top: 1px !important;
        }
    }
    span.title {
        padding-top: 0 !important;
        padding-left: 20px !important;
        font-size: 24px !important;
        padding-bottom: 1rem;
    }
    .m-order-first {
        order: 1 !important;
        padding-right: unset !important;
        padding-left: unset !important;
    }
    .m-order-second {
        order: 2 !important;
    }
    .m-order-third {
        order: 2 !important;
    }
    .mp-0 {
        padding: 0 0 0 0 !important;
    }
    .card-body {
        background: transparent !important;
        padding-left: 30px !important;
        padding-right: 27px !important;
        padding-bottom: 0 !important;
    }
    .card-header {
        padding-left: 1rem !important;
        padding-right: 1rem !important;
        & .split-right.fuel:after {
            right: 16% !important;
            height: 21px !important;
            top: 0% !important;
        }
        & .split-right.engine:after {
            right: 5% !important;
            height: 22px !important;
            top: 0% !important;
        }
        & .split-right.torque:after {
            display: none !important;
        }
        & .acc-headers {
            padding-left: 0 !important;
            padding-right: 0 !important;
            &:not(.advice-price) {
                padding-bottom: 1rem !important;
            }
            &:first-child {
                white-space: nowrap !important;
                padding-bottom: 0 !important;
            }
            &:nth-child(2),
            &:nth-child(5) {
                text-align: center !important;
            }
        }
    }
    .tiles {
        margin-bottom: 2px !important;
        margin-right: 0 !important;
        width: 100% !important;
        & a {
            font-size: 18px !important;
            padding: 5% 0% !important;
            & .brochure {
                max-height: 30px !important;
                min-height: 30px !important;
                max-width: 21px !important;
            }
            & .configure {
                max-width: 32px !important;
                max-height: 26px !important;
                min-height: 26px !important;
            }
            & .drive {
                max-width: 30px !important;
                max-height: 30px !important;
                min-height: 30px !important;
            }
            & .car-large {
                max-width: 27.6px !important;
                max-height: 24.2px !important;
                min-height: 24.2px !important;
            }
        }
    }
    .card-header.collapsed {
        & .after-arrow:after {
            right: 20px;
            top: 50px;
        }
    }
    .card-header {
        & .after-arrow:after {
            right: 20px;
            top: 50px;
        }
    }
}

@media (max-width: $breakpoint-mobile) {
    .showroomOverlay{
        justify-content: center;
        align-items: center;
        text-align: center;
        display: none !important;
        position: fixed;
        width: 100%;
        height: 100vh;
        z-index: 99999; 
        background-color: rgba(0, 0, 0, 0.9);
        top: 0;
        left: 0;
        
        .showroomContainer{
            width: 100%;
            min-height: 600px;
    
            div {
                iframe {
                    width: 100% !important;
                    min-height: 600px;
                    overflow: hidden !important;
                }
            }
    
            .frame-header{
                width: 100%; 
                padding-left: 8px; 
                padding-right: 8px;
                padding-bottom: 2px;
    
            }
    
            .frame-title{
                font-family: DaimlerCAC;
                font-size: 24px;
                font-weight: normal;
                font-stretch: normal;
                font-style: normal;
                line-height: normal;
                letter-spacing: normal;
                color: #ffffff;
            }
    
            .iframeClose{
                padding-left: 10px;
                padding-right: 5px;
                font-weight: 900;
                font-size: 24px;
              &:hover{
                  cursor: pointer;
              }
            }
        }
    }
}

@media (max-width: $breakpoint-desktop) {}

@media (max-width: $breakpoint-huge) {}
